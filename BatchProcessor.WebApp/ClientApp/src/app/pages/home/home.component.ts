import { Component } from '@angular/core';
import { NotificationService } from 'src/app/services/notification.service';
import { ProcessorService } from 'src/app/services/processor.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(
    private notifier: NotificationService,
    private processService: ProcessorService) {

  }

  amountOfBatches: number;
  amountOfNumbersPerBatch: number;
  processing: boolean;

  startProcess(): void {
    if (!this.validateInputs())
      return;

    this.processing = true;
    this.processService.startProcess(this.amountOfBatches, this.amountOfNumbersPerBatch)
      .subscribe(
        data => console.log(data),
        error => console.error("An error occurred while getting data."),
        () => console.log("completed"));
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
