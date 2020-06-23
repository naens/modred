\ data structures for the editor

\ Single line in the buffer
struct
   cell% field buffer-line-prev		\ previous line
   cell% field buffer-line-next		\ next line
   cell% field biffer-line-text		\ link to the contents of the line
end-struct buffer-line%

\ The buffer
struct
   cell% field buffer-line		\ line number
   cell% field buffer-name		\ buffer display name
   cell% field buffer-filename		\ name of the associated file
   buffer-line% field buffer-first-line	\ link to the first line
   char% field buffer-modified		\ modified flag
   char% field buffer-readonly		\ buffer is read-only flag
   char% field buffer-draft		\ buffer is a draft flag
end-struct buffer%
