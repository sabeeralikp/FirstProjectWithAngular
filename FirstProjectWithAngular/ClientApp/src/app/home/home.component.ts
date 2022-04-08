import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { FormBuilder } from "@angular/forms";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
})
export class HomeComponent implements OnInit {
  books: BookModel[] = [];
  booksFormGroup = this.fb.group({
    bookID: [],
    bookName: [],
    author: [],
    bookPrice: [],
  });
  isCreate: Boolean = false;
  constructor(private http: HttpClient, private fb: FormBuilder) {}
  ngOnInit(): void {
    this.http
      .get<BookModel[]>("https://localhost:5001/Books/Read")
      .subscribe((data) => {
        console.log(data);
        this.books = data;
      });
  }
  newBook() {
    this.isCreate = true;
    this.booksFormGroup.reset();
  }
  createBook() {
    if (this.booksFormGroup.get("bookID").value == null)
      this.booksFormGroup.get("bookID").patchValue(0);
    let fd = new FormData();
    fd.append("bookID", this.booksFormGroup.get("bookID").value);
    fd.append("bookName", this.booksFormGroup.get("bookName").value);
    fd.append("author", this.booksFormGroup.get("author").value);
    fd.append("bookPrice", this.booksFormGroup.get("bookPrice").value);
    fd.append("PhotoPath", this.file);
    this.http
      .post<BookModel>("https://localhost:5001/Books/Create", fd)
      .subscribe((data) => {
        this.isCreate = false;
        this.ngOnInit();
        this.ngOnInit();
      });
  }
  editBook(book: BookModel) {
    this.isCreate = true;
    this.booksFormGroup.get("bookID").patchValue(book.bookID);
    this.booksFormGroup.get("bookName").patchValue(book.bookName);
    this.booksFormGroup.get("author").patchValue(book.author);
    this.booksFormGroup.get("bookPrice").patchValue(book.bookPrice);
  }
  deleteBook(bookID: Number) {
    this.http
      .get<Number>("https://localhost:5001/Books/Delete/?bookID=" + bookID)
      .subscribe((data) => {
        this.ngOnInit();
      });
  }
  file: any;
  onFileChange($event) {
    this.file = $event.target.files[0];
  }
  resetForm() {
    this.booksFormGroup.reset();
  }
}

export interface BookModel {
  bookID: number;
  bookName: string;
  author: string;
  bookPrice: number;
  photoPath: string;
}
