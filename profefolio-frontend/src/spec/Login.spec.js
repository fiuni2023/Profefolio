import React from "react"
import { render, screen } from "@testing-library/react"
import'@testing-library/jest-dom'
import Login from '../pages/login'
import { Simulate } from "react-dom/test-utils";

jest.mock('react-router-dom', ()=>{
    const nav = jest.fn();
    return {
        ...jest.requireActual('react-router-dom'),
        useLocation: jest.fn(()=>({pathname: '/example'})),
        useNavigate: jest.fn(()=>nav),
    };
});

const Router = require('react-router-dom')



describe("Test del Login", ()=>{

    let mail
    let password 
    let btn

    const mockSetState = jest.fn();

    jest.mock('react', ()=>({
        useState: initial => [initial, mockSetState]
    }));
    
    beforeEach(()=>{
        render(<Login />)
        jest.clearAllMocks();
        mail = screen.getByPlaceholderText(/Correo Electrónico/i);
        password = screen.getByPlaceholderText(/Contraseña/i);
        btn = screen.getByRole("button", {name: "Ingresar"});
    })

    test("existe el textbox del correo", ()=>{
        
        expect(mail).toBeInTheDocument();

    })

    test("existe el textbox del password", ()=>{
        
        expect(password).toBeInTheDocument();

    })

    test("existe el boton Ingresar", ()=>{
        
        expect(btn).toBeInTheDocument();

    })

    test("el boton cambia el state", async () => {
        const mockCallBack = jest.fn()
        
        Simulate.click(btn)
        expect(mockCallBack).toBeCalled()

    })

})