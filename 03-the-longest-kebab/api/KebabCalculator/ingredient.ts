import { CookingUnit } from "./enums"

interface IIngredient {
    name: string;
    unit: CookingUnit;
    amount: number;
}

export {
    IIngredient
}