import { BarbecueItem } from "./barbecue-item";

export class BarbequeCostResult {
  name: string;
  barbequeItemsTotalCost: number;
  costPerPerson: number;
  remainingBudget: number;
  barbecueItems: BarbecueItem;
}
