import React from 'react'
import { BsFillPlusCircleFill } from 'react-icons/bs'

const BtnAdd = ({ handleShowModal = () => { } }) => {
    //console.log(handleShowModal)
    return <>
        <div className='btnAddContainer'>
            <div className="btnAddFloating">
                <button className="buttonNavBarA" onClick={handleShowModal}>
                    <BsFillPlusCircleFill />
                </button>
            </div>

        </div>

        <style jsx="true">
            {
                `
                .btnAddContainer{
                    display: flex;
                    justify-content: flex-end;
                }
            `
            }
        </style>
    </>
}

export default BtnAdd