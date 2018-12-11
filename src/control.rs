use textbuf::TextBuf;

#[derive(Debug)]
pub enum State {
    Command,
    Insert
}

#[derive(Debug)]
pub struct Control {
    textbuf: TextBuf,
    state: State,
}

impl Control {
    pub fn new(file_in: &str, file_out: &str) -> Control {
        let mut textbuf = TextBuf::new();
        Control {
            textbuf: textbuf,
            state: State::Insert
        }
    }

    pub fn execute(&self, cmd: &str) {
        println!("{:?}", self);
        println!("Execute: {}", cmd);
    }

    pub fn get_prompt(&self) -> &str {
        "     : *"
    }
}
