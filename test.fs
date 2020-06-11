\ test: input: a single string
\       output: linked list of lines, relative line number
include data.fs
include do-edit.fs

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

\ test editing functionality
s" 0123456789" do-edit

cr ." Line offset: " . cr
." Text: " cr
display-text
." ---END---"

\ restore terminal
s" stty " terminal-state s+ system

quit
