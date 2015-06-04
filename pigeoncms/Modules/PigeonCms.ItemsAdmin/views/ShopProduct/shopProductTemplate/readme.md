#Spiegazione Folders:

build: contiene tutti i files relativi al building (subfiles per Grunt)

frontend: contiene tutti i file per lo sviluppo frontend

  -> assets

  -> handlers: per eventuali API

  -> views: contiene html e import vari

site: dentro ci va pigeon e/o altro code backend

  -> www: è la www del sito

static: qui dentro viene compilato lo statico (dist folder)

packages Sublime Text 3:
	- sublimelinter
	- sublimelinter-jscs
	- sublimelinter-contrib-scss-lint

npm-modules:
	- npm install -g jscs (https://packagecontrol.io/packages/SublimeLinter-jscs, http://sublimelinter.readthedocs.org/en/latest/troubleshooting.html#finding-a-linter-executable)

gems:
	- gem install scss_lint(https://gist.github.com/luislavena/f064211759ee0f806c88)
	look for ssl_certs (C:\Ruby21-x64\lib\ruby\2.1.0\rubygems\ssl_certs)



add to editor config: {
	"detect_indentation": true,
	"tab_size": 4,
	"translate_tabs_to_spaces": true,
	"trim_trailing_white_space_on_save": true
}
