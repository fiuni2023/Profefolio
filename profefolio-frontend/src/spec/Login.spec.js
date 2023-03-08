import { render, screen } from "@testing-library/react"
import'@testing-library/jest-dom'
import Login from '../pages/login'

describe("Test del componente Login", ()=>{

    let mail
    let password 
    let btn
    
    beforeEach(()=>{
        render(<Login />)
        mail = screen.getByPlaceholderText(/Correo Electrónico/i)
        password = screen.getByPlaceholderText(/Contraseña/i)
        btn = screen.getByRole("button", {name: "Ingresar"})
    })
    
    test("existe el textbox del correo", ()=>{
        
        expect(mail).toBeInTheDocument()

    })

    test("existe el textbox del password", ()=>{
        
        expect(password).toBeInTheDocument()

    })

    test("existe el boton Ingresar", ()=>{
        
        expect(btn).toBeInTheDocument()

    })

})