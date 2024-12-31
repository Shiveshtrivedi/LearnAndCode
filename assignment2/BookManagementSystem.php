//book class does not follow srp becuase it also save the files 
class Book {
 
 function getTitle() {
     return "A Great Book";
 }

 function getAuthor() {
     return "John Doe";
 }

 function turnPage() {
     // pointer to next page
 }

 function getCurrentPage() {
     return "current page content";
 }

 function getLocation() {
     // returns the position in the library
     // ie. shelf number & room number
 }
}

class BookSaver {
    function save(Book $book) {
        $filename = '/documents/'. $book->getTitle(). ' - ' . $book->getAuthor();
        file_put_contents($filename, serialize($book));
    }
}


interface Printer {

 function printPage($page);
}

class PlainTextPrinter implements Printer {

 function printPage($page) {
     echo $page;
 }

}

class HtmlPrinter implements Printer {

 function printPage($page) {
     echo '<div style="single-page">' . $page . '</div>';
 }
}