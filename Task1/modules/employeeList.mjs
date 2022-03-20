import { Employee } from "./employee.mjs";
import { BASE_URL } from "./constants.mjs";

const employeeListItem = document.getElementById("employees");

export class EmployeeList {
  fetchEmployees() {
    while (employeeListItem.firstChild) {
      employeeListItem.firstChild.remove();
    }
    fetch(BASE_URL)
      .then((response) => response.json())
      .then((data) => {
        this.loadEmployees(data);
      });
  }

  loadEmployees(data) {
    this.employeeList = data.map((e) => {
      const emp = new Employee();
      emp.setId(e.id);
      emp.setName(e.name);
      emp.setSurname(e.surname);
      emp.setEmail(e.email);
      emp.setPhone(e.phone);

      emp.createEmployee();
    });
  }
}
