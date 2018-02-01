var SimInitShader = {

    uniforms: {
        "tDiffuse": { type: "t", value: null },
        "uColor": { type: "f", value: new THREE.Vector4(1.0, 1.0, 1.0, 1.0) }
    },

    vertexShader: Utils.loadTextFile("/ParticlesContent/shaders/Basic.vs.glsl"),

    fragmentShader: Utils.loadTextFileInject("/ParticlesContent/shaders/SimInitShader.fs.glsl")

};