import { interval, Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ProcessModel } from 'src/app/models/process.model';
import { NotificationService } from 'src/app/services/notification.service';
import { ProcessorService } from 'src/app/services/processor.service';

@Component({
  selector: 'app-process-page',
  templateUrl: './process.component.html',
})
export class ProcessPageComponent implements OnInit {

  constructor(
    private notifier: NotificationService,
    private processService: ProcessorService) {
  }
  async ngOnInit() {
    const processId = localStorage.getItem('process-id');
    if (processId) {
      this.processing = true;
      this.updateProcess(processId);
    }
  }

  process: ProcessModel;
  amountOfBatches: number;
  amountOfNumbersPerBatch: number;
  processing: boolean;

  async startProcess(): Promise<any> {
    if (!this.validateInputs())
      return;

    this.processing = true;
    this.process = await this.processService.createProcess(this.amountOfBatches, this.amountOfNumbersPerBatch);

    localStorage.setItem('process-id', this.process.id);

    await this.queueProcess(this.process.id);

    this.updateProcess(this.process.id);
  }

  updateProcess(processId: string) {
    const processUpdate = interval(2000).subscribe(async () => {
      this.process = await this.processService.getStatus(processId);
      if (this.process.isFinished) {
        this.processing = false;
        localStorage.removeItem('process-id');
        processUpdate.unsubscribe();
      }
    });
  }

  async queueProcess(processId: string) {
    await this.processService.queueProcess(processId);
  }

  validateInputs(): boolean {
    if (!this.amountOfBatches || (this.amountOfBatches <= 0 && this.amountOfBatches > 10)) {
      this.notifier.warning('Amount of batches needs to be a value between "1" and "10".');
      return false;
    }
    if (!this.amountOfNumbersPerBatch || this.amountOfNumbersPerBatch <= 0 || this.amountOfNumbersPerBatch > 10) {
      this.notifier.warning('Amount of numbers per batch needs to be a value between "1" and "10".');
      return false;
    }

    return true;
  }
}
