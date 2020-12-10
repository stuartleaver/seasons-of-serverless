import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

import { KebabRecipeService } from 'src/app/services/kebab-recipe.service';
import { KebabRecipe } from 'src/app/components/longest-kebab/kebab-recipe';
import { CookingUnit, LengthUnit } from 'src/app/components/longest-kebab/enums'

import { faInfoCircle, faUtensils } from '@fortawesome/free-solid-svg-icons';
import { ThrowStmt } from '@angular/compiler';

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
  loading: boolean;
  errors: string;

  faInfoCircle = faInfoCircle;
  faUtensils = faUtensils;

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
      [CookingUnit.Whole, ''],
      [CookingUnit.Clove, 'cloves']
    ]);

    this.lengthUnitLabel = new Map<number, string>([
      [LengthUnit.cm, 'cm\'s'],
      [LengthUnit.m, 'm\'s'],
      [LengthUnit.km, 'km\'s']
    ]);
  }

  onSubmit(formData) {
    this.errors = null;
    this.kebabRecipe = null
    this.loading = true;

    const kebabRecipe = this.kebabRecipeService.getKebabRecipe(formData.baseIngredientWeight).subscribe(kebabRecipe => {
      this.kebabRecipe = kebabRecipe;
    },
    error => {
      this.errors = error;

      this.loading = false;
    },
    () => {
      this.loading = false;
    });
  }
}
