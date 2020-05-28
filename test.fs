\ example buffer with four lines:
\ 1: (16) "1 The First Line"
\ 2: (0) ""
\ 3: (11) "3 line3text"
\ 4: (2) "4."

include data.fs
include do-edit.fs

\ create structures
create buf1 text-buffer% %allot drop

\ TODO: allocate lines and strings dynamically, so that they can be freed.
create line1 line% %allot drop
create line2 line% %allot drop
create line3 line% %allot drop
create line4 line% %allot drop

\ fill buffer
buf1 text-buffer-line 3 swap !
buf1 text-buffer-filename 0 swap !
buf1 text-buffer-first-line line1 swap !
buf1 text-buffer-modified false swap !
buf1 text-buffer-readonly false swap !
buf1 text-buffer-draft true swap !

\ fill line1
line1 line-prev 0 swap !
line1 line-next line2 swap !
line1 line-text here swap ! ," 1 The First Line"

\ fill line2
line2 line-prev line1 swap !
line2 line-next line3 swap !
line2 line-text here swap ! ," "

\ fill line3
line3 line-prev line2 swap !
line3 line-next line4 swap !
line3 line-text here swap ! ," 3 line3text"

\ fill line4
line4 line-prev line3 swap !
line4 line-next 0 swap !
line4 line-text here swap ! ," 4."


\ test editing functionality
buf1 do-edit
