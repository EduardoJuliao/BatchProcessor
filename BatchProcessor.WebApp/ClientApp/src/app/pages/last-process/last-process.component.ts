import { Component, OnInit } from '@angular/core';
import { ProcessModel } from 'src/app/models/process.model';
import { ProcessorService } from 'src/app/services/processor.service';

@Component({
   selector: 'app-last-process-page',
   templateUrl: './last-process.component.html'
})

export class LastProcessPageComponent implements OnInit {
   process: ProcessModel;
   processing: boolean = false;

   constructor(private processService: ProcessorService) { }

   async ngOnInit() {
      this.processing = true;
      this.process = await this.processService.getLastProcess();
      this.processing = false;
   }
}