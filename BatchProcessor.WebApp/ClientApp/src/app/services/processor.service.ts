import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { ProcessModel } from '../models/process.model';

@Injectable({ providedIn: 'root' })
export class ProcessorService {
   constructor(private http: HttpClient, @Inject('API_URL') private baseUrl: string) { }

   async createProcess(amountOfBatches: number, amountOfNumbersPerBatch: number): Promise<any> {
      return await this.http
         .post(`${this.baseUrl}/api/process/create/${amountOfBatches}/${amountOfNumbersPerBatch}`, {})
         .toPromise();
   }

   async getLastProcess(): Promise<any> {
      return await this.http
         .get(`${this.baseUrl}/api/process/last`)
         .toPromise();
   }

   async queueProcess(processId: string): Promise<any> {
      return await this.http
         .post(`${this.baseUrl}/api/process/queue/${processId}`, {})
         .toPromise();
   }

   async getStatus(processId: string): Promise<any> {
      return await this.http
         .get(`${this.baseUrl}/api/process/status/${processId}`)
         .toPromise();
   }

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