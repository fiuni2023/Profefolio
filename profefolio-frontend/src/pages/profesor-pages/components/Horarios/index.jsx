import React from 'react'

const Horarios = () => {
    return <>
        <div className="container-visualizacion">
            Horarios
        </div>

        <style jsx="true">
            {
                `
                    .container-visualizacion{
                        border: 1px solid black;
                        border-radius: 20px;
                        background-color: gray;
                        min-height: 300px;
                        padding: 1rem;
                    }
                `
            }
        </style>
    </>
}

export default Horarios