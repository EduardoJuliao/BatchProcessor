import { BatchModel } from "./batch.model";

export interface ProcessModel {
   id: string;
   startedAt: Date;
   finishedAt: Date;
   isFinished: boolean;
   batchSize: number;
   numbersPerBatch: number;
   batches: BatchModel[];
}
