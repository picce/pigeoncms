/**
 * Grunt build tasks
 */
/*jshint node:true, camelcase:false */
module.exports = function(grunt) {
    'use strict';

    var path = require('path'),
    //load configurations
        confPaths = grunt.file.readYAML(path.join(__dirname, 'build/grunt-config/paths.yml')),
        confHosts = grunt.file.readYAML(path.join(__dirname, 'build/grunt-config/hosts.yml')),
        confProperties = grunt.file.readYAML(path.join(__dirname, 'build/grunt-config/properties.yml')),
        _ = require('lodash'),
        gruntConfig;

    //up a folder to the project root
    grunt.file.setBase(__dirname);

    //forcing `--gruntfile` flag to current Gruntfile.js
    //since using `.setBase` changes working folder and
    //concurrent tasks won't find Gruntfile.js anymore
    grunt.option('gruntfile', __filename);

    //require all the thing
    require('time-grunt')(grunt);
    require('load-grunt-tasks')(grunt);

    //if `--base` argument is passed in
    //switch to the build folder
    //cannot be done earlier since when working on Phing etc
    //node_modules aren't checked in nor installed
    if (grunt.option('base')) {
        grunt.file.setBase(grunt.option('base'));
    }

    gruntConfig = require('load-grunt-config')(grunt, {

        configPath: path.join(process.cwd(), 'build', 'grunt-tasks'),

        data: {

            pkg: grunt.file.readJSON('package.json'),

            /**
             * Project Metadata (unused)
             * ===============================
             */
            meta: {},

            properties: confProperties,


            /**
             * Project Paths Configuration
             * ===============================
             */
            paths: confPaths,


            /**
             * Remote Hosts Configuration
             * ===============================
             */
            hosts: confHosts

        }
    });


    if (confProperties.notify) {
        grunt.task.run('notify_hooks');
    }


    ['views', 'stylesheets'].forEach(function (buildSection) {
        var engine = confProperties.engines[buildSection];
        grunt.registerTask('_' + buildSection, function (target) {
            var tasks = [];
            if (Array.isArray(engine)) {
                tasks = engine.map(function (eng) {
                    return eng + ':' + (target || 'dev');
                });
            } else {
                tasks = engine + ':' + (target || 'dev');
            }
            grunt.task.run(tasks);
        });
    });

    grunt.registerTask('default', 'Default task', function (target) {
        var tasks = ['dev'],
            properties = grunt.config.get('properties'),
            connectOpts = grunt.config.get('connect.dev.options'),
            ports = grunt.config.get('hosts.devbox.ports');

        function pushMiddleware(middlewareConf, middlewareFn) {
            if (_.isFunction(middlewareConf)) {
                return _.wrap(middlewareConf, function(oldMiddleWares) {
                    var mids = oldMiddleWares.apply(oldMiddleWares, _.toArray(arguments).slice(1));
                    // inject a custom middlewareConf into the array of default middlewares
                    mids.unshift(middlewareFn);
                    return mids;
                });
            } else if (Array.isArray(middlewareConf)) {
                return [middlewareFn].concat(middlewareConf);
            } else {
                return function(connect, options, middlewares) {
                    // inject a custom middlewareConf into the array of default middlewares
                    middlewares.unshift(middlewareFn);
                    return middlewares;
                };
            }
        }

        if (properties.sync) {
            var bs = require('browser-sync').init([], { logSnippet: false, port: ports.browsersync });
            var browserSyncMiddleware = require('connect-browser-sync')(bs);
            connectOpts.middleware = pushMiddleware(connectOpts.middleware, browserSyncMiddleware);
        }

        if (properties.livereload) {
            connectOpts.livereload = ports.livereload;
        }
        if (properties.remoteDebug) {
            //add weinre to the concurrent tasks list
            grunt.config.set('concurrent.dev', (grunt.config.get('concurrent.dev') || []).concat(['weinre:dev']));
            connectOpts.middleware = pushMiddleware(connectOpts.middleware, require('connect-weinre-injector')({
                port: ports.weinre
            }));
        }

        grunt.config.set('connect.dev.options', connectOpts);

        if (target === 'server') {
            tasks.push('connect:dev');
        }

        //this always comes last
        //also if we are running just one task, we don't need concurrency
        var concurrentTasks = grunt.config.get('concurrent.dev');
        if (Array.isArray(concurrentTasks) && concurrentTasks.length === 1) {
            tasks.push(concurrentTasks[0]);
        } else {
            tasks.push('concurrent:dev');
        }

        grunt.task.run(tasks);

    });
};
