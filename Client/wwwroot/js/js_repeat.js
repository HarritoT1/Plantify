
function togglePassword() {
  const passwordField = document.getElementById("floatingPassword");
  const type = passwordField.type === "password" ? "text" : "password";
  passwordField.type = type;
}
function validateOrderInput() {
  if (document.getElementById("orderName").checkValidity() && document.getElementById("orderName").value.trim() !== "") {
    document.getElementById("orderName").classList.remove("is-invalid");
    document.getElementById("enviarOrder").disabled = false;
  } else {
    document.getElementById("orderName").classList.add("is-invalid");
    document.getElementById("enviarOrder").disabled = true;
  }
}
function trigUICarr() {
  document.getElementById("carritoGIF").classList.add("d-block");
}
