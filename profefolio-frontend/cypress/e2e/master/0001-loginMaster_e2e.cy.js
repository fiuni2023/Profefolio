const url = "http://localhost:3000/"
let loginData = {};





describe("Login Master", () => {
    /* it("La API debe responder con el status code 200 si todo esta bien", () => {
        cy.request({
            method: "POST",
            url: `https://localhost:7063/login`,
            body: {
                email: "Carlos.Torres123@mail.com",
                password: "Carlos.Torres123",
            },
        })
            .then((response) => {
                expect(response.status).to.equal(200)
                expect(response.body).to.have.property('token')
                console.log(response.body.email)
                loginData = response.body
            })
    }) */

    /*it("Cargar el Login", () => {
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
*/
    /* it("Loguearse con las Credenciales Agregadas", () => {
        cy.visit(url)
        cy.get("input[type='text'][placeholder='Correo Electrónico']").type("Carlos.Torres123@mail.com")
        cy.get("input[type='password'][placeholder='Contraseña']").type("Carlos.Torres123")
        cy.get("input[type='checkbox'][value='false']").check()
        cy.get("button").contains("Ingresar").click()


    })
 */
    it("Ir a lista de colegios y editar el nombre del primero", () => {
        cy.visit(url)
        /*console.log(loginData)

        cy.window().then((win) => {
            win.localStorage.setItem('loginData', JSON.stringify(loginData));
            cy.reload()
            // Aquí puedes hacer lo que quieras con el valor obtenido, como realizar aserciones
            // o utilizarlo en otras partes de tu prueba.
        }); */
        cy.get("input[type='text'][placeholder='Correo Electrónico']").type("Carlos.Torres123@mail.com")
        cy.get("input[type='password'][placeholder='Contraseña']").type("Carlos.Torres123")
        cy.get("input[type='checkbox'][value='false']").check()
        cy.get("button").contains("Ingresar").click()

        /* cy.get("h3").contains("Bienvenido, Carlos.Torres123")
        cy.get("span").contains("Carlos.Torres123")

        cy.get("div[background='orange']").contains("Colegios")
        cy.get("div").contains("Ultimos Colegios")
        cy.get("table > thead > tr > th").contains("Nombre")

        cy.get("div[background='orange']").contains("Administradores")
        cy.get("div").contains("Ultimos Administradores")
        cy.get("table > thead > tr > th").contains("Nombre y Apellido")
 */
        cy.get('.sc-kZGvTt.eJvIXL.row').children().first().click();

        cy.get("tbody").children().first().click();
        cy.get("input[type='text'][placeholder='Ingrese el nombre'][id=nombreColegio]").click();
        let originalValue = "";
        cy.get("input[type='text'][placeholder='Ingrese el nombre'][id=nombreColegio]").invoke('val').then(text => {
            // `text` variable will contain the text inputted by the user
            console.log(text);
            originalValue = text
        });
        cy.get("input[type='text'][placeholder='Ingrese el nombre'][id=nombreColegio]").type(`${originalValue} - edited for cypress`)

        cy.get("button[type='submit']").contains("Guardar").click()

        //cy.get("input[type='text'][placeholder='Ingrese el nombre'][id=nombreColegio]").click();
        //cy.get("input[type='text'][placeholder='Ingrese el nombre'][id=nombreColegio]").type(`${originalValue})

    })
})