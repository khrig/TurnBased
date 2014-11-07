using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurnBased {
    public class UI {
        private readonly WorldViewSettings worldViewSettings;
        private readonly WorldController worldController;

        public UI(WorldViewSettings worldViewSettings, WorldController worldController) {
            this.worldViewSettings = worldViewSettings;
            this.worldController = worldController;
        }

        private KeyboardState lastKeyBoardState;
        private MouseState lastMouseState;

        public void HandleInput() {
            lastKeyBoardState = HandleKeyBoard();
            lastMouseState = HandleMouse();
        }

        private KeyboardState HandleKeyBoard() {
            KeyboardState currentKeyBoardState = Keyboard.GetState();
            if (lastKeyBoardState.IsKeyDown(Keys.S) && currentKeyBoardState.IsKeyUp(Keys.S)) {
                worldController.AddAction("shoot");
            }
            if (lastKeyBoardState.IsKeyDown(Keys.Space) && currentKeyBoardState.IsKeyUp(Keys.Space)) {
                worldController.AddAction("weapon");
            }
            if (lastKeyBoardState.IsKeyDown(Keys.G) && currentKeyBoardState.IsKeyUp(Keys.G)) {
                worldController.AddAction("changeCharacter");
            }
            return currentKeyBoardState;
        }

        private MouseState HandleMouse() {
            MouseState currentMouseState = Mouse.GetState();
            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released) {
                if (ClickIsOnGrid(currentMouseState)) {
                    Vector2 gridCoordinates = ConvertToGridCoordinates(currentMouseState.X, currentMouseState.Y);
                    worldController.AddAction("move;" + gridCoordinates.X + "," + gridCoordinates.Y);
                }
                else if (ClickIsOnUI(currentMouseState)) {
                    
                }
            }
            return currentMouseState;
        }

        private bool ClickIsOnUI(MouseState currentMouseState) {
            return false;
        }

        private bool ClickIsOnGrid(MouseState currentMouseState) {
            return true;
        }

        private Vector2 ConvertToGridCoordinates(int x, int y) {
            return new Vector2(x / worldViewSettings.TileSizeX, y / worldViewSettings.TileSizeY);
        }
    }
}
