const url = "http://localhost:3000/"
describe("Login Master", () => {
    it("Cargar el Login", () => {
        cy.visit(url)
        cy.get("input[type='text'][placeholder='Correo Electrónico']")
        cy.get("input[type='password'][placeholder='Contraseña']")
        cy.get("input[type='checkbox'][value='false']")
        cy.get("button").contains("Ingresar")
    })

    it("Cargar las Credenciales", () => {
        cy.visit(url)
        cy.get("input[type='text'][placeholder='Correo Electrónico']").type("Carlos.Torres123@mail.com")
        cy.get("input[type='password'][placeholder='Contraseña']").type("Carlos.Torres123")
        cy.get("input[type='checkbox'][value='false']").check()
        cy.get("button").contains("Ingresar")
    })

    it("Loguearse con las Credenciales Agregadas", () => {
        cy.visit(url)
        cy.get("input[type='text'][placeholder='Correo Electrónico']").type("Carlos.Torres123@mail.com")
        cy.get("input[type='password'][placeholder='Contraseña']").type("Carlos.Torres123")
        cy.get("input[type='checkbox'][value='false']").check()
        cy.get("button").contains("Ingresar").click()
    })
})