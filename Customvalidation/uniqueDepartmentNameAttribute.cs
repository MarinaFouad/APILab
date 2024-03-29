﻿using Lab1.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Lab1.CustomValidation
{
    public class UniqueDepartmentNameAttribute : ValidationAttribute
    {
       

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
             var db = (IDepartmentRepo)validationContext.GetService(typeof(IDepartmentRepo));
             var departmentName = (string)value;
            
             
             var departmentId = (int)validationContext.ObjectInstance.GetType().GetProperty("Id").GetValue(validationContext.ObjectInstance);
             var department = db.GetById(departmentId);

            if (db.GetAll().Any(d => d.Name == departmentName))
            {
                return new ValidationResult("Department name must be unique.");
            }

            return ValidationResult.Success;
        }


    }
}
