var EventEmitter = require('events').EventEmitter;

var emitter = new EventEmitter();

emitter.setMaxListeners(0)

module.exports = emitter;