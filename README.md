# base64

Command line utility for base64 exploration

## Default functionality

Encode, from UTF8 String, to Base64 String

	$ base64 hello
	aGVsbG8=
	
## Options

### `--ascii`

Treat the input string as ASCII instead of UTF8

### `--binary`

Encode from space-separated decimal bytes instead of string

	$ base64 --binary 0 1 0
	AAEA
	$ base64 --binary 255 200 101
	/8hl
	
### `--decode`

Decode (to hex byte run) instead of encoding

	$ base64 --decode c3RlZ3JpZmY=
	73-74-65-67-72-69-66-66


### `--to-ascii`

(Requires `--decode`)

Decode to ASCII instead of hex bytes

	$ base64 --decode --to-ascii c3RlZ3JpZmY=
	stegriff


### `--to-utf8`

(Requires `--decode`)

Decode to UTF8 instead of hex bytes

	$ base64 --decode --to-utf8 c3RlZ3JpZmY=
	stegriff
	
(No difference in the example because all the letters of 'stegriff' are inside the ASCII range)