package test1;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;

public class Parser {
    private File file;

    public synchronized void setFile(File f) {
        file = f;
    }
    
    public Parser(File f){
    file = f;
    }
    
    public synchronized File getFile() {
        return file;
    }

    public String getContent() throws IOException {
        FileInputStream i = new FileInputStream(file);
        String output = "";

        try {
            int data;
            while ((data = i.read()) > 0) {
                output += (char) data;
            }
            return output;
        } finally {
            if (i != null) {
                try {
                    i.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    public String getContentWithoutUnicode() throws IOException {
        FileInputStream i = new FileInputStream(file);
        String output = "";
        int data;
        while ((data = i.read()) > 0) {
            if (data < 0x80) {
                output += (char) data;
            }
        }
        return output;
    }

    public void saveContent(String content) {
        FileOutputStream o = null;
        try {
            o = new FileOutputStream(file);
            try {
                for (int i = 0; i < content.length(); i += 1) {
                    o.write(content.charAt(i));
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        } catch (FileNotFoundException ex) {
            ex.printStackTrace();
        } finally {
            if (o != null) {
                try {
                    o.close();
                } catch (IOException ex) {
                    ex.printStackTrace();
                }
            }
        }
    }
}
