pub struct TextBuf {
    text: Vec<String>,
    line: u32,
    position: u32
}

impl TextBuf {
    // insert text at current position
    // position = 0 => append first line
    fn insert(&mut self, text: Vec<String>) {
        // TODO
    }

    // delete lines
    fn delete_lines(&mut self, n: u32) {
        // TODO
        self.position = 0;
    }

    // delete chars
    fn delete_chars(n: u32) {
        // TODO
    }

    // go to line absolute
    fn set_line(&mut self, line: u32) {
        self.line = line;
        self.position = 0;
    }

    // go to line relative
    fn move_line(&mut self, line: i32) {
        let mut self_line = self.line as i32;
        if self_line < line {
            self_line = 0;
        } else {
            self_line += line;
        }
        self.line = self_line as u32;
        self.position = 0;
    }

    // go to character relative
    fn move_position(&mut self, position: i32) {
        let mut self_position = self.position as i32;
        if self_position < position {
            self_position = 0;
        } else {
            self_position += position;
        }
        self.position = self_position as u32;
    }
}
