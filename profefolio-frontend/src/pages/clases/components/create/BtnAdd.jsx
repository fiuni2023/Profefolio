import React from 'react'
import { GrAddCircle } from 'react-icons/gr'

const BtnAdd = ({ handleShowModal = () => { } }) => {
    //console.log(handleShowModal)
    return <>
        <div className='btnAddContainer'>
            <div className="buttonNavBarAdmin">
                <button className="buttonNavBarAdmin" onClick={handleShowModal}>
                    <GrAddCircle />
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
                .buttonNavBarA {
                    border: none;
                    outline: none;
                  }

                  .buttonNavBarAdmin {
                    font-weight: bold;
                    width: 100%;
                    height: 100%;
                    outline: none;
                    border: none;
                    background-color: #F3E6AE;
                    font-size: 20px;
                    color: #C2C2C2;
                  }



            `
            }
        </style>
    </>
}

export default BtnAdd