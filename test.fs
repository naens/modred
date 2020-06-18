\ test: input: a single string
\       output: linked list of lines, relative line number
include data.fs
include command-mode.fs

\ display the text
: display-text ( first-line% -- )
   0 over ( line% counter line% )
   begin
      0<> while ( line% counter )
      dup . ." '"
      over line-text count type ." '" cr
      1+ swap
      line-next @ tuck
   repeat ;

\ save terminal state
s" stty -g" r/o open-pipe throw slurp-fid 2constant terminal-state

\ allow ctrl-z and ctrl-z
s" stty intr undef; stty susp undef" system

\ TODO: send message to the command mode to initialize
\       a buffer containing the string "0123456789"

\ test editing functionality
\ TODO: send message to the command mode to execute the following:
\ s" 0123456789" insert-mode

cr ." Line offset: " . cr
." Text: " cr
\ TODO: send message to the command mode to display the text
\       of the current buffer
display-text
." ---END---"

\ restore terminal
s" stty " terminal-state s+ system

quit
