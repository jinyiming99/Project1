using UnityEngine;
using Game;
using Game.Characters;
using Game.Input;
using GameFrameWork;
namespace Game.Characters
{
    public class MainCharacter : CharacterBase<MainCharacterData>
    {
        protected void Start()
        {
            CharacterControllerStroage.SMainCharacterCharacterController.BindObject(this);
        }

        public void Init()
        {
            LoadMesh("Assets/Res/Cube.prefab");
        }

        public void MoveLeft()
        {
            Vector3 off = Vector3.left * m_data.m_speed * Time.deltaTime;
            m_transform.position = m_transform.position + off;
        }

        public void MoveRight()
        {
            Vector3 off = Vector3.right * m_data.m_speed * Time.deltaTime;
            m_transform.position = m_transform.position + off;
        }
        
        public void MoveForward()
        {
            Vector3 off = Vector3.forward * m_data.m_speed * Time.deltaTime;
            m_transform.position = m_transform.position + off;
        }
        
        public void MoveBack()
        {
            Vector3 off = Vector3.back * m_data.m_speed * Time.deltaTime;
            m_transform.position = m_transform.position + off;
        }
    }
}