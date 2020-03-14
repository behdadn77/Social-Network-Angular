import { Component, Input, Inject, EventEmitter } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthenticationService } from '../authentication.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls:['./post.component.css']
})
export class PostComponent {
  @Input() title: string;
  @Input() body: string;
  @Input() likeCount: number;
  @Input() commentCount: number;
  @Input() imageBase64: string;
  @Input() name: string;
  @Input() isPostOwner:boolean ;
  @Input() id: number;
  hasImg: boolean = false;
  show: boolean = false;
  constructor(private http: HttpClient, @Inject('BASE_URL') private url: string) {

  }
  ngOnInit() {
    this.hasImg = this.imageBase64 != "" ? true : false;
  }
  delete() {
    //let httpParams = new HttpParams().set('id', this.id.toString());
    //let options = { params: httpParams };
    this.http.delete<boolean>(this.url + 'Api/TimeLinePost/'+this.id).subscribe()
  }
}
