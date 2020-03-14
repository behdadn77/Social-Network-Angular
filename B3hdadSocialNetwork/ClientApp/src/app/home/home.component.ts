import { Component, Inject } from '@angular/core';
import { postModel } from '../model/postModel';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  page: number = 0;
  postList: postModel[]=new Array<postModel>();
  constructor(private http: HttpClient, @Inject('BASE_URL') private url: string) {
  }
  loadMorePosts() {
    ++this.page;
    this.http.get<postModel[]>(this.url + 'api/TimeLinePost/GetList/' + this.page).subscribe(result => {
      //this.postList = result;
      for (var i = 0; i < result.length; i++) {
        this.postList.push(result[i]);
      }
    }, error => console.error(error));
  }
  ngOnInit() {
    this.loadMorePosts();
  }
  onScrollDown() {
    console.log('scrolled down');
    this.loadMorePosts();
  }
  onScrollUp() {
    console.log('scrolled up');
  }
}
