import { EmployeeList } from "./modules/employeeList.mjs";
import { Employee } from "./modules/employee.mjs";
import { EmpValidator } from "./modules/empValidator.mjs";

const newEmpBtn = document.querySelector(".new-emp-btn");
const asideContainers = document.querySelectorAll(".aside-container");
const addEmpForm = document.getElementById("add-emp-form");
const editEmpForm = document.getElementById("edit-emp-form");
const employeeListItem = document.getElementById("employees");
const submitBtn = document.querySelector("[data-submit-btn]");

// View Input Elements
const viewIdInp = document.querySelector("[data-view-id]");
const viewNameInp = document.querySelector("[data-view-name]");
const viewSurnameInp = document.querySelector("[data-view-surname]");
const viewEmailInp = document.querySelector("[data-view-email]");
const viewPhoneInp = document.querySelector("[data-view-phone]");

newEmpBtn.addEventListener("click", () => {
  visibleViewContainer();
});

employeeListItem.addEventListener("click", (e) => {
  if (e.target.id === "delete-btn") {
    const id = e.target.closest(".employee").id;
    const employee = new Employee();
    employee.deleteEmployeeById(id);
  } else if (e.target.id === "edit-btn") {
    const id = e.target.closest(".employee").id;
    const employee = new Employee();
    employee.getEmployeeById(id).then(() => visibleEditContainer(employee));
  }
});

addEmpForm.addEventListener("submit", (e) => {
  e.preventDefault();
  const name = e.target.name.value;
  const surname = e.target.surname.value;
  const email = e.target.email.value;
  const phone = e.target.phone.value;

  const validator = new EmpValidator();
  const isValidName = validator.isValidName(name);
  const isValidSurname = validator.isValidSurname(surname);

  if (!isValidName || !isValidSurname) {
    alert("Correct your mistakes");
  }
  const emp = new Employee();
  emp.setName(name);
  emp.setSurname(surname);
  emp.setEmail(email);
  emp.setPhone(phone);
  emp.insertEmployee();
});

editEmpForm.addEventListener("submit", (e) => {
  e.preventDefault();
  const id = e.target.id.value;
  const name = e.target.name.value;
  const surname = e.target.surname.value;
  const email = e.target.email.value;
  const phone = e.target.phone.value;

  const validator = new EmpValidator();
  const isValidName = validator.isValidName(name);
  const isValidSurname = validator.isValidSurname(surname);

  if (!isValidName || !isValidSurname) {
    alert("Correct your mistakes");
  }

  const empInputObj = { id, name, surname, email, phone };
  validator.isDifferent(empInputObj).then((isDifferent) => {
    if (isDifferent) {
      const emp = new Employee();
      emp.setId(id);
      emp.setName(name);
      emp.setSurname(surname);
      emp.setEmail(email);
      emp.setPhone(phone);
      emp.updateEmployee().then((response) => {
        if (response.status === 200) {
          const empList = new EmployeeList();
          empList.fetchEmployees();
        }
      });
    }
  });
});

function visibleViewContainer() {
  const addEmpFormContainer = asideContainers[0];
  addEmpFormContainer.style.visibility = "visible";
}

function visibleEditContainer(data = null) {
  const editEmpFormContainer = asideContainers[1];
  editEmpFormContainer.style.visibility = "visible";

  const isEmpty = EmpValidator.isEmpty(data);
  if (!isEmpty) {
    viewIdInp.value = data.id;

    viewNameInp.value = data.name;
    viewNameInp.disabled = false;

    viewSurnameInp.value = data.surname;
    viewSurnameInp.disabled = false;

    viewEmailInp.value = data.email;
    viewEmailInp.disabled = false;

    viewPhoneInp.value = data.phone;
    viewPhoneInp.disabled = false;

    submitBtn.disabled = false;
  }
}

const empList = new EmployeeList();
empList.fetchEmployees();
