export interface IChat {
  Id: string;
  Sender: string;
  SenderId: string;
  Receiver: string;
  ReceiverId: string;
  Message: string;
  Read: boolean;
  TimeStamp: Date;
}
