var UVMapShader = {

    uniforms: {
    },

    vertexShader: Utils.loadTextFileInject(
        "/ParticlesContent/shaders/UVMapShader.vs.glsl"
    ),

    fragmentShader: Utils.loadTextFileInject(
        "/ParticlesContent/shaders/UVMapShader.fs.glsl"
    )

};