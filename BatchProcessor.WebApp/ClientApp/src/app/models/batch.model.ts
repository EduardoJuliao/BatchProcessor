import { NumberModel } from "./number.model";

export interface BatchModel {
   id: string;
   processId: string;
   order: number;
   size: number;
   sum: number;
   processedNumbers: number;
   remainingNumbers: number;
   numbers: NumberModel[];
}