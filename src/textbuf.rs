pub struct TextBuf {
    text: Vec<String>,
    line: usize,
    position: usize
}

impl TextBuf {

    fn new() -> TextBuf {
        TextBuf {
            text: vec![],
            line: 0,
            position: 0
        }
    }

    fn empty_insert(&mut self, text: Vec<&str>) {
        for (i,l0) in text.iter().enumerate() {
            let line = String::from(*l0);
            self.text.insert(self.line + i, line);
        }
    }

    // insert text at current position
    // position = 0 => append first line
    fn insert(&mut self, text: Vec<&str>) {
        if self.text.len() == 0 {
            self.empty_insert(text);
        } else {
            if text.len() > 0 {
                let mut before;
                let mut after;
                {
                    let curr_line = &mut self.text[self.line];
                    let (b0, a0) = curr_line.split_at(self.position);
                    before = String::from(b0);
                    after = String::from(a0);
                }
                before.push_str(text[0]);
                self.text[self.line] = before;

                let text_len = text.len();
                for i in 1..text_len {
                    let line = String::from(text[i]);
                    self.text.insert(self.line + i, line);
                }

                // last element: append ca
                self.text[self.line + text.len() - 1].push_str(&after);
            }
        }
    }

    // delete lines
    fn delete_lines(&mut self, n: usize) {
        for i in 0..n {
            self.text.remove(n);
        }
        self.position = 0;
    }

    // delete chars
    fn delete_chars(&mut self, n: usize) {
        if self.text.len() > 0 {
            self.text[self.line].remove(self.position);
        }
    }

    // go to line absolute
    fn set_line(&mut self, line: usize) {
        self.line = line;
        self.position = 0;
    }

    // go to line relative
    fn move_line(&mut self, line: i32) {
        let mut self_line = self.line as i32;
        if self_line + line < 0 {
            self_line = 0;
        } else {
            self_line += line;
        }
        self.line = self_line as usize;
        self.position = 0;
    }

    // go to character relative
    fn move_position(&mut self, position: i32) {
        let mut self_position = self.position as i32;
        if self_position + position < 0 {
            self_position = 0;
        } else {
            self_position += position;
        }
        self.position = self_position as usize;
        println!("self.position = {}", self.position);
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_insert() {
        let mut tb = TextBuf::new();
        tb.insert(vec!["abc\n", "de\n", "fgh"]);
        assert_eq!(tb.text, &["abc\n", "de\n", "fgh"]);

        tb.move_line(1);
        tb.insert(vec!["kk\n", "t"]);
        assert_eq!(tb.text, &["abc\n", "kk\n", "tde\n", "fgh"]);

        tb.move_line(1);
        tb.move_position(1);
        tb.insert(vec!["yy\n", "q"]);
        assert_eq!(tb.text, &["abc\n", "kk\n", "tyy\n", "qde\n", "fgh"]);

        tb.move_line(-2);
        tb.move_position(2);
        tb.insert(vec!["q\n", ""]);
        assert_eq!(tb.text, &["abq\n", "c\n", "kk\n", "tyy\n", "qde\n", "fgh"]);

        tb.insert(vec!["uuu"]);
        assert_eq!(tb.text, &["abuuuq\n", "c\n", "kk\n", "tyy\n", "qde\n", "fgh"]);
    }

    fn test_delete_lines() {
        let mut tb = TextBuf::new();
        tb.insert(vec!["abc\n", "de\n", "fgh"]);
        assert_eq!(tb.text, &["abc\n", "de\n", "fgh"]);

        tb.move_line(1);
        tb.delete_lines(1);
        assert_eq!(tb.text, &["abc\n", "fgh"]);

        tb.insert(vec!["yy\n", "qq\n", "z"]);
        assert_eq!(tb.text, &["abc\n", "yy\n", "qq\n", "zfgh"]);

        tb.delete_lines(2);
        assert_eq!(tb.text, &["abc\n", "fgh"]);
    }

    fn test_delete_chars() {
        let mut tb = TextBuf::new();
        tb.insert(vec!["abc\n", "de\n", "fgh"]);
        assert_eq!(tb.text, &["abc\n", "de\n", "fgh"]);

        tb.move_line(1);
        tb.delete_chars(1);
        assert_eq!(tb.text, &["abc\n", "e\n", "fgh"]);

        tb.insert(vec!["yy\n", "qq\n", "z"]);
        assert_eq!(tb.text, &["abc\n", "eyy\n", "qq\n", "zfgh"]);

        tb.delete_chars(2);
        assert_eq!(tb.text, &["abc\n", "e\n", "qq\n", "zfgh"]);
    }

}
