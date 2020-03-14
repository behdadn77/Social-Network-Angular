import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, retry } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/authentication.service';

@Component({
  selector: 'app-update-status',
  templateUrl: './update-status.component.html',
  styleUrls: ['./update-status.component.css'],
})
export class StatusComponent {

  model: UpdateStatus = new UpdateStatus();
  visible: Observable<boolean>;
  constructor(private http: HttpClient, @Inject('BASE_URL') private url: string, authService:AuthenticationService) {
    this.visible = authService.isSignedIn();
  }
  onImageSelected(event) {
    this.model.selectedImage = <File>event.target.files[0];
  }
  update() {
    const fd = new FormData();
    if (this.model.selectedImage!=null) {
      fd.append('image', this.model.selectedImage, this.model.selectedImage.name);
    }
    fd.append('title', this.model.title);
    fd.append('body', this.model.body);
    this.http.post<boolean>(this.url + 'Api/TimeLinePost', fd).subscribe(
      result => {
        this.model = new UpdateStatus();
        window.location.href = "/";
      },
      error => {
        alert('Ops! Somthing went wrong!');
        console.error(`Error: ${JSON.stringify(error)}`)
      }
    )
  }
}
class UpdateStatus{
  selectedImage: File = null;
  title: string="";
  body: string="";
}
