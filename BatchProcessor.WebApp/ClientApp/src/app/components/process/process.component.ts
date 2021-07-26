import { ProcessModel } from 'src/app/models/process.model';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-process',
  templateUrl: './process.component.html',
  styleUrls: ['./process.component.css']
})
export class ProcessComponent implements OnInit {

  @Input('process-data')
  process: ProcessModel;

  @Input('show-remaining')
  showRemaining: boolean = true;

  @Input('show-processed')
  showProcessed: boolean = false;
  constructor() { }

  ngOnInit() {
  }

}
