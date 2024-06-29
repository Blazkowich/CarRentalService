export interface IChat {
  id: string;
  sender: string;
  senderId: string;
  receiver: string;
  receiverId: string;
  message: string;
  read: boolean;
  timeStamp: Date;
}
