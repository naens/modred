\ This module is for testing that the line module works

include line.fs

: line-new-test
   line-new            ( line% )
   dup line-get-string ( line% string )
   swap line-delete    ( string )
   count nip 0= ;

\ TODO: line-insert-character-test
\ TODO: line-delete-character-test
\ TODO: line-delete-test

: line-set-string-test \ new line with a string inside
   line-new                     ( line% )
   dup s" abcd" line-set-string ( line% )
   dup line-get-string          ( line% string )
   swap line-delete             ( string )
   count s" abcd" str= ;
   
: test-line \ run all tests
   line-new-test if ." line-new-test fail" cr then
   line-set-string-test if ." line-set-string-test fail" cr then
   ;
