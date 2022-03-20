import { BASE_URL } from "./constants.mjs";
import { EmployeeList } from "./employeeList.mjs";
import { EmpValidator } from "./empValidator.mjs";

const empItemTemplate = document.querySelector("[data-emp-template]");
const employeeListItem = document.getElementById("employees");

export class Employee {
  async getEmployeeById(id) {
    const response = await fetch(BASE_URL + `/${id}`);
    const data = await response.json();

    if (!EmpValidator.isEmpty(data)) {
      this.id = data.id;
      this.name = data.name;
      this.surname = data.surname;
      this.email = data.email;
      this.phone = data.phone;
    }
  }

  deleteEmployeeById(id) {
    fetch(BASE_URL + `/${id}`, {
      method: "DELETE",
    });

    const empList = new EmployeeList();
    empList.fetchEmployees();
  }

  createProfileImg(name, surname) {
    return name.charAt(0).toUpperCase() + surname.charAt(0).toUpperCase();
  }

  createEmployee() {
    const empItem = empItemTemplate.content.cloneNode(true).children[0];
    const fullname = empItem.querySelector("[data-fullname]");
    const email = empItem.querySelector("[data-email]");
    const phone = empItem.querySelector("[data-phone]");
    const profileImg = empItem.querySelector("[data-profile]");

    fullname.textContent = this.name + " " + this.surname;
    email.textContent = this.email;
    phone.textContent = this.phone;
    profileImg.textContent = this.createProfileImg(this.name, this.surname);
    empItem.id = this.id;

    employeeListItem.append(empItem);
  }

  generateId() {
    return Math.floor(Math.random() * 100_000) + 1;
  }

  insertEmployee() {
    this.id = this.generateId();
    fetch(BASE_URL, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(this),
    });

    const empList = new EmployeeList();
    empList.fetchEmployees();
  }

  async updateEmployee() {
    const response = await fetch(BASE_URL + `/${this.id}`, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(this),
    });

    return response;
  }

  setId(id) {
    this.id = id;
  }

  setName(name) {
    this.name = name;
  }

  setSurname(surname) {
    this.surname = surname;
  }

  setPhone(phone) {
    this.phone = phone;
  }

  setEmail(email) {
    this.email = email;
  }
}
