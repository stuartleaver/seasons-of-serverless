import { CookingUnit } from "./enums"

interface IIngredient {
    name: string;
    unit: CookingUnit;
    amount: string;
}

export {
    IIngredient
}