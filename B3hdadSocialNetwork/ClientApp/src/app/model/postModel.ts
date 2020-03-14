import { Observable } from "rxjs";

export class postModel {
  id: number;
  title: string;
  body: string;
  imageBase64: string;
  dateTime: Date;
  likeCount: number;
  commentCount: number;
  userFirstName: string;
  userLastName: string;
  isPostOwner: boolean;
}
