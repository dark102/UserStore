﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Forms;

namespace UserStore.Models
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [DisplayName("Фамилия")]
        [DataType(DataType.Text)]
        public string? Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [DisplayName("Имя")]
        [DataType(DataType.Text)]
        public string? Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [DisplayName("Отчество")]
        [DataType(DataType.Text)]
        public string? Patronymic { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [DisplayName("Дата рождения")]
        public string? DateOfBirth { get; set; }

        /// <summary>
        /// Номер паспорта(вместе с серией в формате ХХХХ ХХХХХХ)
        /// </summary>
        [DisplayName("Номер паспорта")]
        [RegularExpression(@"\d{4} \d{6}")]
        public string? PassportNumber { get; set; }

        /// <summary>
        /// Место рождения
        /// </summary>
        [DisplayName("Место рождения")]
        [DataType(DataType.Text)]
        public string? PlaceOfBirth { get; set; }

        /// <summary>
        /// Телефон(в формате 7ХХХХХХХХХХ)
        /// </summary>
        [DisplayName("Телефон")]
        [RegularExpression(@"^[7]\d{10}")]
        public string? Phone { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        [DisplayName("Почта")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        /// <summary>
        /// Адрес регистрации
        /// </summary>
        [DisplayName("Адрес регистрации")]
        [DataType(DataType.Text)]
        public string? RegistrationAddress { get; set; }

        /// <summary>
        /// Адрес проживания
        /// </summary>
        [DisplayName("Адрес проживания")]
        [DataType(DataType.Text)]
        public string? ResidenceAddress { get; set; }

    }
}
