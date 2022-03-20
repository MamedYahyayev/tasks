import { Employee } from "./employee.mjs";

const nameErrItem = document.getElementById("err-name");
const surnameErrItem = document.getElementById("err-surname");

export class EmpValidator {
  static isEmpty(empObj) {
    if (
      empObj === null ||
      empObj.name === undefined ||
      empObj.surname === undefined ||
      empObj.email === undefined ||
      empObj.phone === undefined
    ) {
      return true;
    }

    return false;
  }

  isValidName(name) {
    if (name.length > 20) {
      nameErrItem.style.display = "inline-block";
      nameErrItem.textContent = "Name length cannot be more than 20";
      return false;
    }
    nameErrItem.style.display = "none";
    return true;
  }

  isValidSurname(surname) {
    if (surname.length > 20) {
      surnameErrItem.style.display = "inline-block";
      surnameErrItem.textContent = "Surname length cannot be more than 20";
      return false;
    }
    surnameErrItem.style.display = "none";
    return true;
  }

  async isDifferent(empObj) {
    const employee = new Employee();
    await employee.getEmployeeById(+empObj.id);
    if (
      employee.name === empObj.name &&
      employee.surname === empObj.surname &&
      employee.email === empObj.email &&
      employee.phone === empObj.phone
    ) {
      return false;
    }
    return true;
  }
}
