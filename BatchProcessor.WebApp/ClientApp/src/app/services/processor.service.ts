import { Injectable } from '@angular/core';
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class ProcessorService {
   constructor() { }

   public startProcess(amountOfBatches: number, amountOfNumbersPerBatch: number): Observable<any> {
      return new Observable<string>((observer) => {
         const url = `https://localhost:5001/api/process/start/${amountOfBatches}/${amountOfNumbersPerBatch}`;

         let eventSource = new EventSource(url);
         eventSource.onmessage = (event) => {
            console.debug('Received event: ', event);
            observer.next(event.data);
         };
         eventSource.onerror = (error) => {
            // readyState === 0 (closed) means the remote source closed the connection,
            // so we can safely treat it as a normal situation. Another way 
            // of detecting the end of the stream is to insert a special element
            // in the stream of events, which the client can identify as the last one.
            if (eventSource.readyState === 0) {
               console.log('The stream has been closed by the server.');
               eventSource.close();
               observer.complete();
            } else {
               observer.error('EventSource error: ' + error);
            }
         }
      });
   }
}