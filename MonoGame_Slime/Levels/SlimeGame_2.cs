using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Slime.GameCore;
using MonoGame_Slime.Physics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;



namespace MonoGame_Slime
{


    public class SlimeGame_2 : SlimeGame, IDisposable
    {


        protected override void AddPlayers()
        {
            // Player Parameters
            var newPlayerPos = worldCenter + new Vector2(0, -200f);
            var playerRadius = 20f;
            var commonMaxDistance = 50f;
            var commonMinDistance = 30f;
            var playerParametersMultiplier = 0.65f;

            // Add players
            var normalColor = Color.White;
            var notNormalColor = Color.Red;

            var player1 = new Player(newPlayerPos, playerRadius, normalColor);
            player1.image = Arts.Player_Normal;
            player1.scale = Vector2.One * 0.32f;

            // Animation
            var gameTime = new GameTime();

            player1.timer_goto_blink.StartTimer(gameTime, 3000);
            player1.timer_goto_normal.StartTimer(gameTime, 3200);


            var player2 = new Player(newPlayerPos + new Vector2(0, -100) * playerParametersMultiplier, playerRadius, normalColor);
            var player3 = new Player(newPlayerPos + new Vector2(87, -50) * playerParametersMultiplier, playerRadius, normalColor);

            var player4 = new Player(newPlayerPos + new Vector2(87, 50) * playerParametersMultiplier, playerRadius, normalColor);
            var player5 = new Player(newPlayerPos + new Vector2(0, 100) * playerParametersMultiplier, playerRadius, normalColor);
            var player6 = new Player(newPlayerPos + new Vector2(-87, 50) * playerParametersMultiplier, playerRadius, normalColor);
            var player7 = new Player(newPlayerPos + new Vector2(-87, -50) * playerParametersMultiplier, playerRadius, normalColor);


            player2.image = Arts.Player_2;
            player3.image = Arts.Player_3;
            player4.image = Arts.Player_4;
            player5.image = Arts.Player_5;
            player6.image = Arts.Player_2;
            player7.image = Arts.Player_3;



            playerList.Add(player2);
            playerList.Add(player3);
            playerList.Add(player4);
            playerList.Add(player5);
            playerList.Add(player6);
            playerList.Add(player7);
            playerList.Add(player1);

            // Physics
            foreach (var player in playerList)
            {
                _collisionComponent.AddPlayer(player);
                _gravityComponent.AddGameObject(player);
                // world.AddObjectToWorldRotationList(player);
            }

            _constraintComponent.AddConstraintPair(player1, player2, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player1, player3, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player1, player4, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player1, player5, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player1, player6, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player1, player7, commonMaxDistance, commonMinDistance);

            _constraintComponent.AddConstraintPair(player2, player3, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player3, player4, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player4, player5, commonMaxDistance, commonMinDistance);
            // _constraintComponent.AddConstraintPair(player5, player6, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player6, player7, commonMaxDistance, commonMinDistance);
            _constraintComponent.AddConstraintPair(player7, player2, commonMaxDistance, commonMinDistance);



        }

        protected override void AddWalls()
        {
            // Wall parameters
            float wallWidth = 750f;
            var boundBoxWallSizeHorizontal = new Vector2(worldSize.X + 2 * wallWidth, wallWidth);
            var boundBoxWallSizeVertical = new Vector2(wallWidth, worldSize.Y + 2 * wallWidth);

            float wallOffSetX = (worldSize.X + wallWidth) / 2f;
            float wallOffsety = (worldSize.Y + wallWidth) / 2f;

            var normalWallTexture = Arts.Wall;
            var spikeTexture = Arts.Spike_1;


           


            // add border walls
            var wall_1 = new NormalWall(worldCenter + new Vector2(0, wallOffsety), boundBoxWallSizeHorizontal, normalWallTexture);
            var wall_2 = new NormalWall(worldCenter + new Vector2(0, -wallOffsety), boundBoxWallSizeHorizontal, normalWallTexture);

            var wall_3 = new NormalWall(worldCenter + new Vector2(wallOffSetX, 0), boundBoxWallSizeVertical, normalWallTexture);
            var wall_4 = new NormalWall(worldCenter + new Vector2(-wallOffSetX, 0), boundBoxWallSizeVertical, normalWallTexture);

            wallList.Add(wall_1);
            wallList.Add(wall_2);
            wallList.Add(wall_3);
            wallList.Add(wall_4);

            // add visual border wall (dont' add to the wallList)

            var wall_left = new NormalWall(worldCenter + new Vector2(wallOffSetX, 0), boundBoxWallSizeVertical, Arts.World);
            var wall_right = new NormalWall(worldCenter + new Vector2(-wallOffSetX, 0), boundBoxWallSizeVertical, Arts.World);



           



            // add pickups and spikes

            var pickup_size = new Vector2(50, 50);
            var pickup_position_offset = new Vector2(400, 200);

            var pickup_1 = new Pickups(worldCenter + pickup_position_offset, pickup_size, Arts.Pickup);
            var pickup_2 = new Pickups(worldCenter - pickup_position_offset, pickup_size, Arts.Pickup);


            var spike_position_offset = pickup_position_offset * new Vector2(1, -1);
            var spike_1 = new Spikes(worldCenter + spike_position_offset, pickup_size * 1.3f, Arts.Spike_3);
            var spike_2 = new Spikes(worldCenter - spike_position_offset, -pickup_size * 1.3f, Arts.Spike_3);


            pickup_1.color = Color.Aqua;
            pickup_2.color = Color.Aqua;


            spike_1.color = Color.Red;
            spike_2.color = Color.Red;

            wallList.Add(pickup_1);
            wallList.Add(pickup_2);

            wallList.Add(spike_1);
            wallList.Add(spike_2);



            // add door

            var door = new Door(worldCenter + new Vector2(0, -300), pickup_size, Arts.Pickup);
            door.color = Color.Yellow;
            wallList.Add(door);



            // Physics
            foreach (var wall in wallList)
            {
                if (!(wall is RotatingWall))
                {
                    world.AddObjectToWorldRotationList(wall);
                }
                _collisionComponent.AddWall(wall);

            }
        }


    }
}
