\ data structures for the editor

\ Single line
struct
   cell% field line-prev		\ previous line
   cell% field line-next		\ next line
   cell% field line-text		\ link to the contents of the line
end-struct line%

\ The buffer
struct
   cell% field text-buffer-line		\ line number
   cell% field text-buffer-name		\ buffer display name
   cell% field text-buffer-filename	\ name of the associated file
   cell% field text-buffer-first-line	\ link to the first line
   char% field text-buffer-modified	\ modified flag
   char% field text-buffer-readonly	\ buffer is read-only flag
   char% field text-buffer-draft	\ buffer is a draft flag
end-struct text-buffer%
