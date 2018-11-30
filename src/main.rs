extern crate rustyline;

use rustyline::error::ReadlineError;
use rustyline::Editor;

mod control; 
mod textbuf;
use control::Control;

fn main() {
    let mut r1 = Editor::<()>::new();
    let control = Control::new();
    loop {
        let prompt = control.get_prompt();
        let readline = r1.readline(prompt);
        match readline {
            Ok(line) => {
                control.execute(&line);
            },
            Err(ReadlineError::Interrupted) => {
                println!("INT");
                break;
            }
            Err(ReadlineError::Eof) => {
                println!("EOF");
                break;
            },
            Err(err) => {
                println!("Error: {:?}", err);
                break;
            }
        }
    }
}
