\ test: input: a single string
\       output: linked list of lines, relative line number
include data.fs
include do-edit.fs

\ save terminal state
s" stty -g" r/o open-pipe throw slurp-fid 2constant terminal-state

\ allow ctrl-z and ctrl-z
s" stty intr undef; stty susp undef" system

\ test editing functionality
buf1 do-edit "0123456789" 10

\ restore terminal
s" stty " terminal-state s+ system
