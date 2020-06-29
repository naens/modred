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

\ TODO: line capacity tests:
\    1. set string to "abcd"
\    2. set string again according to the following (line_chunk_size=16)
\    3. test the capacity, the string and the length of the line
\ TODO:    -> test when new capacity = 0
\ TODO:    -> test when new capacity < current 
\ TODO:    -> test when new capacity = current
\ TODO:    -> test when new capacity < current + line-chunk-size
\ TODO:    -> test when new capacity > current + line-chunk-size
\ TODO:    -> test when new capacity > 255 (illegal string)

: line-set-string-test ( addr u -- bool )
   line-new                     ( addr u line% )
   dup 2over line-set-string    ( addr u line% )
   dup line-get-string          ( addr u line% string )
   swap line-delete count       ( aadr u addr1 u1 )
   str= ;
   
: test-line \ run all tests
   line-new-test if ." line-new-test fail" cr then
   s" abcd" line-set-string-test if ." line-set-string-test fail" cr then
   ;
