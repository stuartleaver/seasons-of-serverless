import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

import { KebabRecipeService } from 'src/app/services/kebab-recipe.service';
import { KebabRecipe } from 'src/app/components/longest-kebab/kebab-recipe';
import { CookingUnit, LengthUnit } from 'src/app/components/longest-kebab/enums'

@Component({
  selector: 'app-longest-kebab',
  templateUrl: './longest-kebab.component.html',
  styleUrls: ['./longest-kebab.component.css']
})
export class LongestKebabComponent implements OnInit {
  kebabRecipe: KebabRecipe;
  kebabForm;
  cookingUnitLabel;
  lengthUnitLabel;

  constructor(
    private kebabRecipeService: KebabRecipeService,
    private formBuilder: FormBuilder
  ) {
    this.kebabForm = this.formBuilder.group({
      baseIngredientWeight: ''
    })
  }

  ngOnInit(): void {
    this.cookingUnitLabel = new Map<number, string>([
      [CookingUnit.Kilos, 'kilos'],
      [CookingUnit.Teaspoons, 'teaspoons'],
      [CookingUnit.Whole, 'whole']
    ]);

    this.lengthUnitLabel = new Map<number, string>([
      [LengthUnit.cm, 'cm\'s'],
      [LengthUnit.m, 'm\'s'],
      [LengthUnit.km, 'km\'s']
    ]);
  }

  onSubmit(formData) {
    const kebabRecipe = this.kebabRecipeService.getKebabRecipe(formData.baseIngredientWeight).subscribe((kebabRecipe => {
      this.kebabRecipe = kebabRecipe;
    }));
  }
}
