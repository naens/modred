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
variable line1 line% %size allocate drop line1 !
\ create line1 line% %allot drop

variable line2 line% %size allocate drop line2 !
variable line3 line% %size allocate drop line3 !
variable line4 line% %size allocate drop line4 !
\ create line2 line% %allot drop
\ create line3 line% %allot drop
\ create line4 line% %allot drop

\ fill buffer
buf1 text-buffer-line 3 swap !
buf1 text-buffer-filename 0 swap !
buf1 text-buffer-first-line line1 @ swap !
buf1 text-buffer-modified false swap !
buf1 text-buffer-readonly false swap !
buf1 text-buffer-draft true swap !

\ fill line1
line1 @ line-prev 0 swap !
line1 @ line-next line2 @ swap !
line1 @ line-text here swap ! ," 1 The First Line"

\ fill line2
line2 @ line-prev line1 @ swap !
line2 @ line-next line3 @ swap !
line2 @ line-text here swap ! ," "

\ fill line3
line3 @ line-prev line2 @ swap !
line3 @ line-next line4 @ swap !
line3 @ line-text here swap ! ," 3 line3text"

\ fill line4
line4 @ line-prev line3 @ swap !
line4 @ line-next 0 swap !
line4 @ line-text here swap ! ," 4."


\ test editing functionality
\ buf1 do-edit

\ create dir 100 allot
\ dir 100 get-dir cr type cr
\ dir 100 get-dir add-lib
\ \c #include "/home/andrei/gproj/modred/rawmode.c"
\ \c #include "./rawmode.c"
\c #include <unistd.h>
\c #include <stdio.h>
\c #include <limits.h>
\c #define STR(s) #s
\c #define STRING(s) STR(s)
\c #define FFF STRING( __FILE__ )
\ \c int pc () {char cwd[100];getcwd(cwd, 100);printf("dir: %s\n", cwd);return 0;}
\c int pc () {
\c   printf("dir: %s\n", FFF);return 0;
\c   return 0;
\c }
\ c-function c_test n -- n

\c #include <stdlib.h>
\c #include <termios.h>
\c #include <unistd.h>
\c #include <stdio.h>
\c #include <ctype.h>
\c
\c struct termios orig_termios;
\c
\c void disable_raw_mode() {
\c   tcsetattr(STDIN_FILENO, TCSAFLUSH, &orig_termios);
\c }
\c
\c int enable_raw_mode () {
\c   tcgetattr(STDIN_FILENO, &orig_termios);
\c   atexit(disable_raw_mode);
\c   struct termios raw = orig_termios;
\c   raw.c_iflag &= ~(BRKINT | ICRNL | INPCK | ISTRIP | IXON);
\c   raw.c_oflag &= ~(OPOST);
\c   raw.c_cflag |= (CS8);
\c   raw.c_lflag &= ~(ECHO | ICANON | IEXTEN | ISIG);
\c   tcsetattr(STDIN_FILENO, TCSAFLUSH, &raw);
\c   return 0;
\c }


\ 123 c_test . cr

c-function pc pc -- n
c-function enable_raw_mode enable_raw_mode -- n

\ pc drop
enable_raw_mode drop

