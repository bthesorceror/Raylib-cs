using Raylib;
using static Raylib.Raylib;
using static Raylib.CameraMode;
using static Raylib.TexmapIndex;

public partial class models_mesh_generation
{
    /*******************************************************************************************
    *
    *   raylib example - procedural mesh generation
    *
    *   This example has been created using raylib 1.8 (www.raylib.com)
    *   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
    *
    *   Copyright (c) 2017 Ramon Santamaria (Ray San)
    *
    ********************************************************************************************/


    public const int NUM_MODELS = 7;

    public static int Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        int screenWidth = 800;
        int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [models] example - mesh generation");

        // We generate a isChecked image for texturing
        Image isChecked = GenImageChecked(2, 2, 1, 1, RED, GREEN);
        Texture2D texture = LoadTextureFromImage(isChecked);
        UnloadImage(isChecked);

        Model[] models = new Model[NUM_MODELS];

        models[0] = LoadModelFromMesh(GenMeshPlane(2, 2, 5, 5));
        models[1] = LoadModelFromMesh(GenMeshCube(2.0f, 1.0f, 2.0f));
        models[2] = LoadModelFromMesh(GenMeshSphere(2, 32, 32));
        models[3] = LoadModelFromMesh(GenMeshHemiSphere(2, 16, 16));
        models[4] = LoadModelFromMesh(GenMeshCylinder(1, 2, 16));
        models[5] = LoadModelFromMesh(GenMeshTorus(0.25f, 4.0f, 16, 32));
        models[6] = LoadModelFromMesh(GenMeshKnot(1.0f, 2.0f, 16, 128));

        // Set isChecked texture as default diffuse component for all models material
        for (int i = 0; i < NUM_MODELS; i++) models[i].material.maps[(int)MAP_ALBEDO].texture = texture;

        // Define the camera to look into our 3d world
        Camera3D camera = new Camera3D(new Vector3( 5.0f, 5.0f, 5.0f ), new Vector3( 0.0f, 0.0f, 0.0f ), new Vector3( 0.0f, 1.0f, 0.0f ), 45.0f, 0 );

        // Model drawing position
        Vector3 position = new Vector3( 0.0f, 0.0f, 0.0f );

        int currentModel = 0;

        SetCameraMode(camera, CameraMode.CAMERA_ORBITAL);  // Set a orbital camera mode

        SetTargetFPS(60);                       // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            UpdateCamera(ref camera);      // Update internal camera and our camera

            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                currentModel = (currentModel + 1)%NUM_MODELS; // Cycle between the textures
            }
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                BeginMode3D(camera);

                    DrawModel(models[currentModel], position, 1.0f, WHITE);

                    DrawGrid(10, 1.0f);

                EndMode3D();

                DrawRectangle(30, 400, 310, 30, Fade(SKYBLUE, 0.5f));
                DrawRectangleLines(30, 400, 310, 30, Fade(DARKBLUE, 0.5f));
                DrawText("MOUSE LEFT BUTTON to CYCLE PROCEDURAL MODELS", 40, 410, 10, BLUE);

                switch(currentModel)
                {
                    case 0: DrawText("PLANE", 680, 10, 20, DARKBLUE); break;
                    case 1: DrawText("CUBE", 680, 10, 20, DARKBLUE); break;
                    case 2: DrawText("SPHERE", 680, 10, 20, DARKBLUE); break;
                    case 3: DrawText("HEMISPHERE", 640, 10, 20, DARKBLUE); break;
                    case 4: DrawText("CYLINDER", 680, 10, 20, DARKBLUE); break;
                    case 5: DrawText("TORUS", 680, 10, 20, DARKBLUE); break;
                    case 6: DrawText("KNOT", 680, 10, 20, DARKBLUE); break;
                    default: break;
                }

            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------

        // Unload models data (GPU VRAM)
        for (int i = 0; i < NUM_MODELS; i++) UnloadModel(models[i]);

        CloseWindow();        // Close window and OpenGL context
        //--------------------------------------------------------------------------------------

        return 0;
    }
}
